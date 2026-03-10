using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create an instance of ExcelSaveOptions
        ExcelSaveOptions excelOptions = new ExcelSaveOptions();

        // Set the output format to Excel 2003 XML (XMLSpreadsheet2003)
        excelOptions.Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003;

        // The instance is now ready for use, e.g., when saving a PDF to Excel:
        // string pdfPath = "input.pdf";
        // string outputPath = "output.xml";
        // using (Document pdfDoc = new Document(pdfPath))
        // {
        //     pdfDoc.Save(outputPath, excelOptions);
        // }
    }
}