using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;

namespace WebAppWare.Models;

public class OrderPdfReport
{
	int _totalColumn = 3;
	Document _document;
	Font _fontStyle;
	PdfPTable _table = new PdfPTable(3);
	PdfPCell _cell;
	MemoryStream _stream = new MemoryStream();
	OrderModel _orderDetails = new OrderModel();
	string _orderNumber;
	DateTime _creationDate;

	public byte[] PrepareReport(OrderModel model)
	{
		_orderDetails = model;
		_orderNumber = _orderDetails.Document;
		_creationDate = _orderDetails.CreationDate;

		_document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
		_document.SetPageSize(PageSize.A4);
		_document.SetMargins(20f, 20f, 20f, 20f);
		_table.WidthPercentage = 100;
		_table.HorizontalAlignment = Element.ALIGN_LEFT;
		_table.SpacingBefore = 10;
		_fontStyle = FontFactory.GetFont("Thaoma", 10f, 1);
		PdfWriter.GetInstance(_document, _stream);
		_document.Open();
		_table.SetWidths(new float[] { 25f, 90f, 60f });
		_table.SpacingAfter = 25;

		ReportHeader();
		ReportBody();
		_table.HeaderRows = 2;
		_document.Add(_table);

		PdfPCell remarksCell = new PdfPCell(new Phrase($"Uwagi do zamówienia: \n\n{_orderDetails.Remarks}", _fontStyle));
		remarksCell.HorizontalAlignment = Element.ALIGN_LEFT;
		remarksCell.Border = 0;
		PdfPTable remarksTable = new PdfPTable(1);
		remarksTable.AddCell(remarksCell);
		remarksTable.CompleteRow();
		_document.Add(remarksTable);

		_document.Close();

		return _stream.ToArray();
	}

	private void ReportHeader()
	{
		_fontStyle = FontFactory.GetFont("Tahoma", 12f, 1);

		Paragraph par1 = new Paragraph("Zamawiający: \n\nFirma X \nul. Wrześniowa 2 \n81-113 Wolsztyn", _fontStyle);
		par1.Alignment = Element.ALIGN_LEFT;
		par1.SpacingAfter = 20;
		_document.Add(par1);

		//Paragraph supplierData = new Paragraph($"{_orderDetails.SupplierName} \n{_orderDetails.OrderDetails[0].SupplierEmail}", _fontStyle);
		//supplierData.Alignment = Element.ALIGN_MIDDLE;
		//supplierData.SpacingAfter = 20;

		

		byte[] imageBytes;

		WebClient wc = new WebClient();
		imageBytes = wc.DownloadData(@"C:\Users\Yahu\source\repos\WebAppWare\WebAppWare\wwwroot\images\test-image.jpg");

		Image img = Image.GetInstance(imageBytes);
		img.ScaleAbsolute(150f, 50f);
		img.SetAbsolutePosition(420, 780);
		//img.SpacingAfter = 20;
		_document.Add(img);

		//PdfPCell cell2 = new PdfPCell();
		//cell2.AddElement(supplierData);
		//cell2.HorizontalAlignment = Element.ALIGN_MIDDLE;
		//PdfPTable table = new PdfPTable(1);
		//table.AddCell(cell2);
		//table.CompleteRow();
		//_document.Add(cell2);

		_fontStyle = FontFactory.GetFont("Tahoma", 12f, 1);
		_cell = new PdfPCell(new Phrase($"Data utworzenia: {_creationDate.ToShortDateString()}", _fontStyle));
		_cell.Colspan = _totalColumn;
		_cell.HorizontalAlignment = Element.ALIGN_LEFT;
		_cell.Border = 0;
		_cell.BackgroundColor = BaseColor.WHITE;
		_cell.ExtraParagraphSpace = 0;
		_cell.ExtraParagraphSpace = 5;
		_table.AddCell(_cell);
		_table.CompleteRow();

		_fontStyle = FontFactory.GetFont("Tahoma", 12f, 1);
		_cell = new PdfPCell(new Phrase($"Nr zamówienia: {_orderNumber}", _fontStyle));
		_cell.Colspan = _totalColumn;
		_cell.HorizontalAlignment = Element.ALIGN_CENTER;
		_cell.Border = 0;
		_cell.BackgroundColor = BaseColor.WHITE;
		_cell.ExtraParagraphSpace = 0;
		_cell.ExtraParagraphSpace = 10;
		_table.AddCell(_cell);
		_table.CompleteRow();

		PdfPCell cell2 = new PdfPCell(new Phrase($"Dostawca: \n\n{_orderDetails.SupplierName} \n{_orderDetails.OrderDetails[0].SupplierEmail}", _fontStyle));
		cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
		cell2.Border = 0;
		PdfPTable table = new PdfPTable(1);
		table.AddCell(cell2);
		table.CompleteRow();
		_document.Add(table);
	}

	private void ReportBody()
	{
		_fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
		_cell = new PdfPCell(new Phrase("Number", _fontStyle));
		_cell.HorizontalAlignment = Element.ALIGN_CENTER;
		_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
		_cell.BackgroundColor = BaseColor.LIGHT_GRAY;
		_table.AddCell(_cell);

		_fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
		_cell = new PdfPCell(new Phrase("Item Code", _fontStyle));
		_cell.HorizontalAlignment = Element.ALIGN_CENTER;
		_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
		_cell.BackgroundColor = BaseColor.LIGHT_GRAY;
		_table.AddCell(_cell);

		//_fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
		//_cell = new PdfPCell(new Phrase("Item Code", _fontStyle));
		//_cell.HorizontalAlignment = Element.ALIGN_CENTER;
		//_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
		//_cell.BackgroundColor = BaseColor.LIGHT_GRAY;
		//_table.AddCell(_cell);

		_fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
		_cell = new PdfPCell(new Phrase("Quantity", _fontStyle));
		_cell.HorizontalAlignment = Element.ALIGN_CENTER;
		_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
		_cell.BackgroundColor = BaseColor.LIGHT_GRAY;
		_table.AddCell(_cell);
		_table.CompleteRow();



		_fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);

		int serialNumber = 1;

		foreach (OrderDetailsModel item in _orderDetails.OrderDetails)
		{
			_cell = new PdfPCell(new Phrase(serialNumber++.ToString(), _fontStyle));
			_cell.HorizontalAlignment = Element.ALIGN_CENTER;
			_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			_cell.BackgroundColor = BaseColor.WHITE;
			_table.AddCell(_cell);

			_cell = new PdfPCell(new Phrase(item.ProductItemCode, _fontStyle));
			_cell.HorizontalAlignment = Element.ALIGN_CENTER;
			_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			_cell.BackgroundColor = BaseColor.WHITE;
			_table.AddCell(_cell);

			//_cell = new PdfPCell(new Phrase(item.ProductItemCode, _fontStyle));
			//_cell.HorizontalAlignment = Element.ALIGN_CENTER;
			//_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			//_cell.BackgroundColor = BaseColor.WHITE;
			//_table.AddCell(_cell);

			_cell = new PdfPCell(new Phrase(item.Quantity.ToString(), _fontStyle));
			_cell.HorizontalAlignment = Element.ALIGN_CENTER;
			_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			_cell.BackgroundColor = BaseColor.WHITE;
			_table.AddCell(_cell);
			_table.CompleteRow();
		}

		//PdfPCell remarksCell = new PdfPCell(new Phrase($"Uwagi do zamówienia: \n\n{_orderDetails.Remarks}", _fontStyle));
		//remarksCell.HorizontalAlignment = Element.ALIGN_LEFT;
		//remarksCell.Border = 0;
		//PdfPTable remarksTable = new PdfPTable(1);
		//remarksTable.AddCell(remarksCell);
		//remarksTable.CompleteRow();
		//_document.Add(remarksTable);
	}
}
