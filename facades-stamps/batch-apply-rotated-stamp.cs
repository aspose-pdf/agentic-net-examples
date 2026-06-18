using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // -------------------------------------------------------------------
        // 1. Create a one‑page PDF that will be used as the stamp.
        // -------------------------------------------------------------------
        using (Document stampDoc = new Document())
        {
            Page stampPage = stampDoc.Pages.Add();
            TextFragment stampText = new TextFragment("CONFIDENTIAL");
            stampText.TextState.FontSize = 48;
            stampText.TextState.FontStyle = FontStyles.Bold;
            stampText.TextState.ForegroundColor = Color.Red;
            stampPage.Paragraphs.Add(stampText);
            stampDoc.Save("stamp.pdf");
        }

        // -------------------------------------------------------------------
        // 2. Create a few sample PDFs that simulate files stored on a network share.
        //    Evaluation mode limits collections to four elements, so we create four.
        // -------------------------------------------------------------------
        string[] targetFiles = new string[] { "doc1.pdf", "doc2.pdf", "doc3.pdf", "doc4.pdf" };
        for (int i = 0; i < targetFiles.Length; i++)
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                TextFragment pageText = new TextFragment("Document " + (i + 1));
                pageText.TextState.FontSize = 12;
                page.Paragraphs.Add(pageText);
                doc.Save(targetFiles[i]);
            }
        }

        // -------------------------------------------------------------------
        // 3. Apply the rotated stamp to each PDF.
        // -------------------------------------------------------------------
        foreach (string inputFile in targetFiles)
        {
            string outputFile = "stamped_" + inputFile;

            // Bind the source PDF (the document that will receive the stamp).
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputFile);

            // Create a stamp from the first page of the stamp PDF.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindPdf("stamp.pdf", 1);
            // Rotate the stamp 45 degrees.
            stamp.Rotation = 45;
            // Place the stamp behind the page content.
            stamp.IsBackground = true;

            // Add the stamp to every page of the source PDF.
            fileStamp.AddStamp(stamp);
            // Save the stamped document.
            fileStamp.Save(outputFile);
            // Release resources.
            fileStamp.Close();
        }

        Console.WriteLine("All documents have been stamped.");
    }
}
