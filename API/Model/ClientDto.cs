﻿using System.ComponentModel.DataAnnotations;

namespace API.Model
{
	public class ClientDto
	{
		[Required(ErrorMessage ="First name is required")]
		public string FirstName { get; set; } = "";

		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; } = "";
		
		[Required, EmailAddress]
		public string Email { get; set; } = "";
		[Phone]
		public string? Phone { get; set; } = "";
		public string? Address { get; set; } = "";
		[Required]
		public string Status { get; set; } = "";
	}
}
