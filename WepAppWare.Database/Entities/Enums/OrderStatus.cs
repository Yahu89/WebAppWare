using System.ComponentModel.DataAnnotations;

namespace WebAppWare.Database.Entities.Enums
{
	public enum OrderStatus
	{
		[Display(Name = "None")]
		None = 0,
		[Display(Name = "W przygotowaniu")]
		InProgress = 1,
		[Display(Name = "Wysłano")]
		Sent = 2,
		[Display(Name = "Zrealizowano")]
		Completed = 3,
		[Display(Name = "Anulowano")]
		Canceled = 4,
	}
}
