using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create an empty PDF document
        Document pdfDoc = new Document();

        // Save the document as a PDF file
        pdfDoc.Save("output.pdf");
    }
}