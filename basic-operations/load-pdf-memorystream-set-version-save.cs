using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a simple PDF in memory (self‑contained example)
        using (var initialDoc = new Document())
        {
            // Add a blank page so the PDF is not empty
            initialDoc.Pages.Add();

            // Save the document to a MemoryStream
            using (var tempStream = new MemoryStream())
            {
                initialDoc.Save(tempStream);
                // Reset the stream position before reading it back
                tempStream.Position = 0;

                // Load PDF from the MemoryStream
                using (var doc = new Document(tempStream))
                {
                    // Change the PDF version to 1.4 using Document.Convert
                    string logPath = "conversion_log.xml"; // optional conversion log
                    doc.Convert(logPath, PdfFormat.v_1_4, ConvertErrorAction.Delete);

                    // Save the modified PDF to the file system
                    doc.Save("output.pdf");
                }
            }
        }

        Console.WriteLine("PDF saved with version 1.4.");
    }
}