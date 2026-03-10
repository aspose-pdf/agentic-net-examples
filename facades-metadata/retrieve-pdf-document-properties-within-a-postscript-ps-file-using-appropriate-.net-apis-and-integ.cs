using System;
using System.Globalization;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string psInputPath = "input.ps";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"File not found: {psInputPath}");
            return;
        }

        // Load the PostScript file as a PDF document using PsLoadOptions
        using (Document psDoc = new Document(psInputPath, new PsLoadOptions()))
        {
            // Retrieve document properties via the PdfFileInfo facade
            PdfFileInfo fileInfo = new PdfFileInfo(psDoc);
            string author = fileInfo.Author;
            string title = fileInfo.Title;
            string subject = fileInfo.Subject;
            string keywords = fileInfo.Keywords;
            string creationDateStr = fileInfo.CreationDate; // string representation
            string modDateStr = fileInfo.ModDate;           // string representation
            int pageCount = fileInfo.NumberOfPages;
            string pdfVersion = fileInfo.GetPdfVersion();

            // Convert the string dates to nullable DateTime values (if possible)
            DateTime? creationDate = ParsePdfDate(creationDateStr);
            DateTime? modDate = ParsePdfDate(modDateStr);

            // Example integration: create a new PDF and copy the retrieved metadata
            using (Document newPdf = new Document())
            {
                // Add a blank page (required for a valid PDF)
                newPdf.Pages.Add();

                // Apply the extracted metadata to the new document
                newPdf.Info.Author = author;
                newPdf.Info.Title = title;
                newPdf.Info.Subject = subject;
                newPdf.Info.Keywords = keywords;
                if (creationDate.HasValue) newPdf.Info.CreationDate = creationDate.Value;
                if (modDate.HasValue) newPdf.Info.ModDate = modDate.Value;

                // Optionally copy the first page from the original document
                if (psDoc.Pages.Count > 0)
                {
                    newPdf.Pages.Add(psDoc.Pages[1]);
                }

                // Save the resulting PDF
                newPdf.Save(outputPdfPath);
            }

            // Output the extracted properties for verification
            Console.WriteLine($"Author: {author}");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Keywords: {keywords}");
            Console.WriteLine($"Pages: {pageCount}");
            Console.WriteLine($"PDF Version: {pdfVersion}");
            Console.WriteLine($"Creation Date: {creationDate?.ToString("u") ?? "N/A"}");
            Console.WriteLine($"Modification Date: {modDate?.ToString("u") ?? "N/A"}");
        }
    }

    /// <summary>
    /// Parses a PDF date string (as returned by PdfFileInfo) into a nullable DateTime.
    /// The PDF date format is typically "yyyyMMddHHmmss" optionally followed by a timezone.
    /// If parsing fails, null is returned.
    /// </summary>
    private static DateTime? ParsePdfDate(string pdfDate)
    {
        if (string.IsNullOrWhiteSpace(pdfDate))
            return null;

        // Try common PDF date formats
        string[] formats = new[]
        {
            "yyyyMMddHHmmssK", // with timezone (e.g., 20230101123000+02'00')
            "yyyyMMddHHmmss",   // without timezone
            "yyyyMMddHHmmsszzz" // .NET timezone format
        };

        if (DateTime.TryParseExact(pdfDate, formats, CultureInfo.InvariantCulture,
                                   DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                                   out DateTime dt))
        {
            return dt;
        }

        // Fallback to generic parsing
        if (DateTime.TryParse(pdfDate, out dt))
            return dt;

        return null;
    }
}