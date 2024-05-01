using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using WebAppWare.Database.Entities;
using WebAppWare.Repositories.Interfaces;

namespace WebAppWare.Models;

public class MovementPdfReport
{
    int _totalColumn = 4;
    Document _document;
    Font _fontStyle;
    PdfPTable _table = new PdfPTable(4);
    PdfPCell _cell;
    MemoryStream _stream = new MemoryStream();
    List<ProductFlowModel> _productFlows = new List<ProductFlowModel>();
    string _movementNumber;
	DateTime _creationDate;

	private readonly IImageRepository _imageRepository;
	public MovementPdfReport(IImageRepository imageRepository)
    {
		_imageRepository = imageRepository;
    }

    public byte[] PrepareReport(List<ProductFlowModel> model)
    {
        _productFlows = model;
		_movementNumber = _productFlows.FirstOrDefault().DocumentNumber;
		_creationDate = _productFlows[0].CreationDate;

        _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
        _document.SetPageSize(PageSize.A4);
        _document.SetMargins(20f, 20f, 20f, 20f);
        _table.WidthPercentage = 100;
        _table.HorizontalAlignment = Element.ALIGN_LEFT;
        _table.SpacingBefore = 10;
        _fontStyle = FontFactory.GetFont("Thaoma", 10f, 1);
        PdfWriter.GetInstance(_document, _stream);
        _document.Open();
        _table.SetWidths(new float[] { 25f, 90f, 60f, 40f });

        ReportHeader();
        ReportBody();
        _table.HeaderRows = 2;
        _document.Add(_table);

		_document.Close();

        return _stream.ToArray();
    }

    private async void ReportHeader()
    {
		_fontStyle = FontFactory.GetFont("Tahoma", 12f, 1);

		Paragraph par1 = new Paragraph("Firma X \nul. Wrześniowa 2 \n81-113 Wolsztyn", _fontStyle);
		par1.Alignment = Element.ALIGN_LEFT;
		par1.SpacingAfter = 20;
		_document.Add(par1);

		byte[] imageBytes;

		WebClient wc = new WebClient();
		
		//imageBytes = wc.DownloadData(@"C:\Users\Yahu\source\repos\WebAppWare\WebAppWare\Images\dev-hobby-logo2442555683.jpg");
		imageBytes = wc.DownloadData(@"C:\Users\Yahu\source\repos\WebAppWare\WebAppWare\wwwroot\images\test-image.jpg");

        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imageBytes);
		img.ScaleAbsolute(150f, 50f);
		img.SetAbsolutePosition(420, 780);
		//img.SpacingAfter = 20;
		_document.Add(img);

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
        _cell = new PdfPCell(new Phrase($"Ruch magazynowy: {_movementNumber}", _fontStyle));
        _cell.Colspan = _totalColumn;
        _cell.HorizontalAlignment = Element.ALIGN_CENTER;
        _cell.Border = 0;
        _cell.BackgroundColor = BaseColor.WHITE;
        _cell.ExtraParagraphSpace = 0;
        _cell.ExtraParagraphSpace = 10;
        _table.AddCell(_cell);
        _table.CompleteRow();
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
		_cell = new PdfPCell(new Phrase("Warehouse", _fontStyle));
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

		_fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
		_cell = new PdfPCell(new Phrase("Quantity", _fontStyle));
		_cell.HorizontalAlignment = Element.ALIGN_CENTER;
		_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
		_cell.BackgroundColor = BaseColor.LIGHT_GRAY;
		_table.AddCell(_cell);
		_table.CompleteRow();

		_fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);

        int serialNumber = 1;

        foreach (ProductFlowModel item in _productFlows)
        {
			_cell = new PdfPCell(new Phrase(serialNumber++.ToString(), _fontStyle));
			_cell.HorizontalAlignment = Element.ALIGN_CENTER;
			_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			_cell.BackgroundColor = BaseColor.WHITE;
            _table.AddCell(_cell);

			_cell = new PdfPCell(new Phrase(item.Warehouse, _fontStyle));
			_cell.HorizontalAlignment = Element.ALIGN_CENTER;
			_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			_cell.BackgroundColor = BaseColor.WHITE;
            _table.AddCell(_cell);

			_cell = new PdfPCell(new Phrase(item.ProductItemCode, _fontStyle));
			_cell.HorizontalAlignment = Element.ALIGN_CENTER;
			_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			_cell.BackgroundColor = BaseColor.WHITE;
            _table.AddCell(_cell);

			_cell = new PdfPCell(new Phrase(item.Quantity.ToString(), _fontStyle));
			_cell.HorizontalAlignment = Element.ALIGN_CENTER;
			_cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			_cell.BackgroundColor = BaseColor.WHITE;
			_table.AddCell(_cell);
			_table.CompleteRow();
		}
    }
}
