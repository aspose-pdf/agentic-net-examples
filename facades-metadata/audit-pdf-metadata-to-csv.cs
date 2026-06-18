using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputPdfPath = "output_updated.pdf"; // PDF after metadata change
        const string csvLogPath    = "metadata_audit.csv"; // CSV audit file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Load original metadata using PdfFileInfo facade
        // -----------------------------------------------------------------
        string origTitle, origAuthor, origSubject, origKeywords, origCreator;
        string origCreationDate, origModDate; // dates are strings in PDF‑date format

        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
        {
            // Capture original values
            origTitle        = pdfInfo.Title;
            origAuthor       = pdfInfo.Author;
            origSubject      = pdfInfo.Subject;
            origKeywords     = pdfInfo.Keywords;
            origCreator      = pdfInfo.Creator;
            origCreationDate = pdfInfo.CreationDate;
            origModDate      = pdfInfo.ModDate;

            // -----------------------------------------------------------------
            // Step 2: Update metadata (example values – adjust as needed)
            // -----------------------------------------------------------------
            pdfInfo.Title   = "New Document Title";
            pdfInfo.Author  = "New Author Name";
            pdfInfo.Subject = "Updated Subject";
            pdfInfo.Keywords = "keyword1;keyword2";
            pdfInfo.Creator = "Aspose.Pdf.Facades Example";

            // PdfFileInfo expects dates as PDF‑date formatted strings, not DateTime.
            string pdfDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            pdfInfo.CreationDate = pdfDate;
            pdfInfo.ModDate      = pdfDate;

            // Save the updated metadata into a new PDF file
            bool saved = pdfInfo.SaveNewInfo(outputPdfPath);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save updated PDF.");
                return;
            }
        }

        // -----------------------------------------------------------------
        // Step 3: Load updated metadata to verify changes
        // -----------------------------------------------------------------
        string newTitle, newAuthor, newSubject, newKeywords, newCreator;
        string newCreationDate, newModDate;

        using (PdfFileInfo updatedInfo = new PdfFileInfo(outputPdfPath))
        {
            newTitle        = updatedInfo.Title;
            newAuthor       = updatedInfo.Author;
            newSubject      = updatedInfo.Subject;
            newKeywords     = updatedInfo.Keywords;
            newCreator      = updatedInfo.Creator;
            newCreationDate = updatedInfo.CreationDate;
            newModDate      = updatedInfo.ModDate;
        }

        // -----------------------------------------------------------------
        // Step 4: Write audit log to CSV (Property, Original, New)
        // -----------------------------------------------------------------
        using (StreamWriter writer = new StreamWriter(csvLogPath, false))
        {
            writer.WriteLine("Property,Original Value,New Value");

            void WriteRow(string property, string original, string updated)
            {
                // Escape double quotes and surround with quotes to protect commas
                string escOrig = original?.Replace("\"", "\"\"");
                string escUpd  = updated?.Replace("\"", "\"\"");
                writer.WriteLine($"{property},\"{escOrig}\",\"{escUpd}\"");
            }

            WriteRow("Title",        origTitle,        newTitle);
            WriteRow("Author",       origAuthor,       newAuthor);
            WriteRow("Subject",      origSubject,      newSubject);
            WriteRow("Keywords",     origKeywords,     newKeywords);
            WriteRow("Creator",      origCreator,      newCreator);
            WriteRow("CreationDate", origCreationDate, newCreationDate);
            WriteRow("ModDate",      origModDate,      newModDate);
        }

        Console.WriteLine($"Metadata audit CSV created at '{csvLogPath}'.");
        Console.WriteLine($"Updated PDF saved at '{outputPdfPath}'.");
    }
}
