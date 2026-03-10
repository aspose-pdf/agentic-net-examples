using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPclPath = "output.pcl"; // PCL output not supported by Aspose.Pdf

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load PDF metadata using the PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
        {
            string title    = pdfInfo.Title;
            string author   = pdfInfo.Author;
            string subject  = pdfInfo.Subject;
            string keywords = pdfInfo.Keywords;

            Console.WriteLine($"Title:    {title}");
            Console.WriteLine($"Author:   {author}");
            Console.WriteLine($"Subject:  {subject}");
            Console.WriteLine($"Keywords: {keywords}");

            // Create a new PDF document and embed the retrieved metadata
            using (Document doc = new Document())
            {
                doc.Info.Title    = title;
                doc.Info.Author   = author;
                doc.Info.Subject  = subject;
                doc.Info.Keywords = keywords;

                // Add a blank page so the document is valid
                doc.Pages.Add();

                // Aspose.Pdf cannot save to PCL format (PclSaveOptions does not exist).
                // Save as PDF instead; replace with appropriate PCL generation if available.
                doc.Save("metadata_embedded.pdf");
            }
        }
    }
}