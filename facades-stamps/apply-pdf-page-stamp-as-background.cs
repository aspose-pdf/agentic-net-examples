using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF to be stamped
        const string stampPdf  = "stamp.pdf";   // PDF containing the stamp page
        const string outputPdf = "output.pdf";  // Resulting PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampPdf))
        {
            Console.Error.WriteLine($"Stamp PDF not found: {stampPdf}");
            return;
        }

        try
        {
            // Load the source document (the one that will receive the stamp)
            Document targetDoc = new Document(inputPdf);

            // Load the document that contains the stamp page (use the first page as the stamp)
            Document stampDoc = new Document(stampPdf);
            Page stampPage = stampDoc.Pages[1]; // assuming the stamp is on the first page

            // Apply the stamp as a background to every page of the target document
            foreach (Page page in targetDoc.Pages)
            {
                // Create a page stamp from the stamp page
                PdfPageStamp pageStamp = new PdfPageStamp(stampPage);
                // Set the stamp to be drawn as a background
                pageStamp.Background = true;
                // Apply the stamp to the current page
                pageStamp.Put(page);
            }

            // Save the stamped document
            targetDoc.Save(outputPdf);

            Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
