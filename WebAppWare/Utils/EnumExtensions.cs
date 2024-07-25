using static QuestPDF.Helpers.Colors;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppWare.Utils
{
	public static class EnumExtensions
	{
		// method extension?
		// czyli rozszerzenie klasy/obietu przez stworzenie metody
		public static string? DisplayName(this Enum value)
		{
			if (value == null)
			{
				return null;
			}

			var field = value.GetType().GetField(value.ToString());
			var attributes = field?.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

			return attributes?.Length > 0
				? attributes[0].Name
				: value.ToString();
		}

		// to metoda, ktora konwertuje dany enum na kolekcje obiektow SelectListItem, gdzie Key to bedzie liczba z enum, a Value to bedzie tego co przekazemy
		public static IEnumerable<SelectListItem> ToSelectList<T>(
			Func<T, string?> translate,
			Func<T, bool>? selected = null
		)
		{
			return Enum.GetValues(typeof(T))
				.Cast<T>()
				.Select(x => new SelectListItem
				{
					Value = x?.ToString(),
					Text = translate(x)?.ToString(),
					Selected = selected == null ? false : selected(x),
				})
				.OrderBy(e => e.Text);
		}
	}
}
