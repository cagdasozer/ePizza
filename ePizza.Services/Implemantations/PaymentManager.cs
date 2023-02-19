using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using ePizza.Services.Interfaces;
using ePizza.Services.Models;
using Microsoft.Extensions.Options;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Implemantations
{
	public class PaymentManager : IPaymentService
	{
		private readonly IOptions<RazorPayConfig> _razorPayConfig;
		private readonly RazorpayClient _client;
		private readonly IRepository<PaymentDetails> _repository;
		private readonly ICartRepository _carRepository;

		public PaymentManager(IOptions<RazorPayConfig> razorPayConfig, RazorpayClient client, IRepository<PaymentDetails> repository, ICartRepository carRepository)
		{
			_razorPayConfig = razorPayConfig;
			_client = client;
			_repository = repository;
			_carRepository = carRepository;
		}

		public string CapturePayment(string paymentId, string orderId)
		{
			if (!string.IsNullOrWhiteSpace(paymentId))
			{
				try
				{
					Payment payment = _client.Payment.Fetch(paymentId);
					Dictionary<string, object> options = new Dictionary<string, object>();
					options.Add("amount", payment.Attributes["amount"]);
					options.Add("currency", payment.Attributes["currency"]);
					Payment paymentCaptured = payment.Capture(options);
					return paymentCaptured.Attributes["status"];
				}
				catch (Exception ex)
				{
					return ex.Message;
				}
			}
			return null;
		}

		public string CrateOrder(decimal amount, string currency, string receipt)
		{
			try
			{
				Dictionary<string, object> options = new Dictionary<string, object>
			{
				{"amount",amount },
				{"currency",currency},
				{"receipt", receipt },
				{"payment_capture",1 }
			};
				Razorpay.Api.Order orderReponse = _client.Order.Create(options);
				return orderReponse["id"].ToString();
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		public Payment GetPaymentDetails(string paymentId)
		{
			if (!string.IsNullOrWhiteSpace(paymentId))
			{
				return _client.Payment.Fetch(paymentId);
			}
			return null;
		}

		public async Task<int> SavePaymentDetails(PaymentDetails model)
		{
			await _repository.AddAsync(model);
			var cart = _carRepository.FindAsync(model.CartId);
			cart.Result.IsACtive = false;
			return await _repository.SaveAsync();
		}

		public bool VerifySignature(string signature, string orderId, string paymentId)
		{
			string payload = $"{orderId}|{paymentId}";
			string secret = RazorpayClient.Secret;
			string actualSignature = GetActualSignature(payload, secret);
			return actualSignature.Equals(signature);
		}

		private static string GetActualSignature(string payload, string secret)
		{
			byte[] secretbytes = StringEncode(secret);
			HMACSHA256 hashHmac = new HMACSHA256(secretbytes);
			var bytes = StringEncode(payload);
			return HashCode(hashHmac.ComputeHash(bytes));
		}

		private static string HashCode(byte[] bytes)
		{
			return BitConverter.ToString(bytes).Replace("_", "").ToLower();
		}

		private static byte[] StringEncode(string secret)
		{
			var encoding = new ASCIIEncoding();
			return encoding.GetBytes(secret);
		}
	}
}
