using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace ShoeStoreClothing.ViewModels
{
	public class UserViewModel
	{
		[Required]
		public string? FullName { get; set; }
		[Required]
		[EmailAddress]
		[Remote(action: "IsEmailExist", controller:"User",areaName:"Admin")]
		public string? Email { get; set; }	
		public string? Password { get; set; }
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
		public string? ConfirmPassword { get; set; }
		public string? Role { get; set; }
	}
}
