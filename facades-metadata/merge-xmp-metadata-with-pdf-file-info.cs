using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class MergeMetadataExample
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load existing document file information (title, author, etc.)
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPdf))
        {
            string title = fileInfo.Title;
            string author = fileInfo.Author;

            // Load XMP metadata associated with the same PDF
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(inputPdf);

                // Merge standard document info into the XMP packet
                if (!string.IsNullOrEmpty(title))
                {
                    // "dc:title" is the Dublin Core title property
                    xmp.Add("dc:title", title);
                }

                if (!string.IsNullOrEmpty(author))
                {
                    // "dc:creator" holds the author/creator information
                    xmp.Add("dc:creator", author);
                }

                // Add a custom XMP property
                xmp.Add("xmp:CustomProperty", "CustomValue");

                // Persist the merged XMP metadata together with the file info
                bool success = fileInfo.SaveNewInfoWithXmp(outputPdf);
                Console.WriteLine(success
                    ? $"Metadata merged and saved to '{outputPdf}'."
                    : "Failed to save merged metadata.");
            }
        }
    }
}