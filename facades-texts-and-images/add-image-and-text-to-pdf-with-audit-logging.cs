using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades types (PdfFileMend, FormattedText, EncodingType)

class Program
{
    // Simple logger that writes to console (could be redirected to a file)
    static void LogOperation(string operation, string fileName, int pageNumber)
    {
        string timestamp = DateTime.Now.ToString("o"); // ISO 8601 format
        Console.WriteLine($"{timestamp} | {operation} | Page: {pageNumber} | File: {fileName}");
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        const string imagePath = "logo.png";          // image to add
        const string textToAdd = "Confidential";     // text to add

        // Coordinates for image and text (lower‑left X/Y, upper‑right X/Y for image;
        // X/Y for text). Adjust as needed.
        const float imgLlX = 50f, imgLlY = 700f, imgUrX = 150f, imgUrY = 800f;
        const float textX = 200f, textY = 750f;

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Initialize PdfFileMend with the loaded document (facade for adding content)
            using (PdfFileMend mend = new PdfFileMend(doc))
            {
                // ---- Add Image -------------------------------------------------
                bool imgResult = mend.AddImage(imagePath, 1, imgLlX, imgLlY, imgUrX, imgUrY);
                LogOperation("AddImage", Path.GetFileName(imagePath), 1);

                // ---- Add Text --------------------------------------------------
                // FormattedText constructor expects System.Drawing.Color and a float font size.
                FormattedText ft = new FormattedText(
                    textToAdd,
                    System.Drawing.Color.Red,
                    "Helvetica",
                    EncodingType.Winansi,
                    false,
                    24f);

                // AddText overload that accepts a single page number.
                bool txtResult = mend.AddText(ft, 1, textX, textY);
                LogOperation("AddText", "FormattedText", 1);
            }

            // Save the modified document (lifecycle rule: save inside using)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdfPath}'.");
    }
}
