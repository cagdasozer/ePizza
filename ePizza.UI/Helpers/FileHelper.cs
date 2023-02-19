using ePizza.UI.Helpers.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ePizza.UI.Helpers
{
	public class FileHelper : IFileHelper
	{
		//IWebHostEnvironment interface bizim için dosya alıp verme işlemleri gerçekleştirmektedir. Bu interface sadece .core tarafında mevcuttur klasik .net frameworkte yoktur...

		private IWebHostEnvironment _env;

		public FileHelper(IWebHostEnvironment env)
		{
			_env = env;
		}
		private string GenerateFileName(string fileName)
		{
			string[] strName = fileName.Split('.');
			string strFileName = DateTime.Now.ToUniversalTime().ToString("ddMMyyyy\\THmmssfff") + "." + strName[strName.Length - 1];
			return strFileName;
		}

		public void DeleteFile(string imageUrl)
		{
			//delete
			if (File.Exists(_env.WebRootPath+imageUrl))
			{
				File.Delete(_env.WebRootPath + imageUrl);
			}
		}

		public string UploadFile(IFormFile file)
		{
			var uploads = Path.Combine(_env.WebRootPath);
			bool exists = Directory.Exists(uploads);
			if (!exists)
				Directory.CreateDirectory(uploads);

			var fileName = GenerateFileName(file.FileName);
			var fileStream = new FileStream(Path.Combine(uploads,fileName),FileMode.Create);
			file.CopyToAsync(fileStream);
			return "/images/" + fileName;
		}
	}
}
