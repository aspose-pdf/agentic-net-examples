using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main(string[] args)
    {
        // Accept PDF file paths via command‑line arguments; if none provided, use a default placeholder.
        string[] pdfFiles = args.Length > 0 ? args : new[] { "input.pdf" };

        foreach (string filePath in pdfFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Ensure deterministic disposal of the Document object.
            using (Document doc = new Document(filePath))
            {
                // Number of fonts used in the document – retrieved via FontUtilities.
                Aspose.Pdf.Text.Font[] fonts = doc.FontUtilities.GetAllFonts();
                int fontCount = fonts?.Length ?? 0;

                // Number of form fields (AcroForm fields) in the document.
                int formFieldCount = doc.Form?.Count ?? 0;

                // Number of tables – only available when the PDF is tagged.
                int tableCount = 0;
                if (doc.TaggedContent != null)
                {
                    ITaggedContent taggedContent = doc.TaggedContent;
                    var tables = taggedContent.RootElement.FindElements<TableElement>(true);
                    tableCount = tables?.Count ?? 0;
                }

                // Log the statistics.
                Console.WriteLine($"Document: {Path.GetFileName(filePath)}");
                Console.WriteLine($"  Fonts       : {fontCount}");
                Console.WriteLine($"  Form fields : {formFieldCount}");
                Console.WriteLine($"  Tables      : {tableCount}");
                Console.WriteLine();
            }
        }
    }
}
