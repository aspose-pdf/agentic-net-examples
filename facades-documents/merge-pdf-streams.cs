using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create sample PDFs
        CreateSamplePdf("sample1.pdf", "First PDF");
        CreateSamplePdf("sample2.pdf", "Second PDF");
        CreateSamplePdf("sample3.pdf", "Third PDF");

        // Open streams for input PDFs and an output stream
        using (FileStream inputStream1 = new FileStream("sample1.pdf", FileMode.Open, FileAccess.Read))
        using (FileStream inputStream2 = new FileStream("sample2.pdf", FileMode.Open, FileAccess.Read))
        using (FileStream inputStream3 = new FileStream("sample3.pdf", FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream("merged.pdf", FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor fileEditor = new PdfFileEditor();
            Stream[] inputStreams = new Stream[] { inputStream1, inputStream2, inputStream3 };
            bool result = fileEditor.Concatenate(inputStreams, outputStream);
            // result is true if concatenation succeeded
        }
    }

    static void CreateSamplePdf(string fileName, string text)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextFragment fragment = new TextFragment(text);
            page.Paragraphs.Add(fragment);
            doc.Save(fileName);
        }
    }
}
