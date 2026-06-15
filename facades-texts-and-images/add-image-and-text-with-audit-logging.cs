using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;
using System.Drawing; // needed for Rectangle used by PdfContentEditor

class Program
{
    // Simple audit logger – writes to console (replace with file logger if needed)
    static void LogOperation(string operation, int pageNumber, string fileName)
    {
        string timestamp = DateTime.Now.ToString("o"); // ISO 8601 format
        Console.WriteLine($"{timestamp} | {operation} | Page: {pageNumber} | File: {fileName}");
    }

    static void Main()
    {
        const string inputPdf   = "source.pdf";
        const string outputPdf  = "modified.pdf";
        const string imagePath  = "logo.png";
        const string textToAdd  = "Confidential";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdf))
        {
            // ---------- Add Image using PdfFileMend ----------
            // Bind the facade to the loaded document
            using (PdfFileMend mend = new PdfFileMend())
            {
                mend.BindPdf(doc);

                // Define image placement (lower‑left and upper‑right coordinates)
                int imagePage = 1;
                float llx = 50f;   // lower‑left X
                float lly = 50f;   // lower‑left Y
                float urx = 150f;  // upper‑right X
                float ury = 150f;  // upper‑right Y

                // Add the image
                bool imageAdded = mend.AddImage(imagePath, imagePage, llx, lly, urx, ury);
                if (imageAdded)
                {
                    // Fully qualified System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
                    LogOperation("AddImage", imagePage, System.IO.Path.GetFileName(imagePath));
                }
                else
                {
                    Console.Error.WriteLine("Failed to add image.");
                }

                // Close the facade (saves changes made through the facade)
                mend.Close();
            }

            // ---------- Add Text using PdfContentEditor ----------
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Define text placement rectangle (System.Drawing.Rectangle is required by CreateText)
                int textPage = 1;
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(200, 700, 200, 50); // x, y, width, height

                // Create a text annotation on the specified page
                // Parameters: rectangle, text, font name, isBold, unique name, page number
                editor.CreateText(rect, textToAdd, "Helvetica", false, "TextAnnotation1", textPage);

                // Log the text addition
                LogOperation("AddText", textPage, "TextAnnotation1");

                // Save changes made by the editor
                editor.Save(outputPdf);
                // No explicit Close needed; using will dispose the editor
            }

            // If additional modifications were made directly on the Document, save it
            // (In this example, the editor already saved the file, but we keep this for completeness)
            if (!File.Exists(outputPdf))
            {
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdf}'.");
    }
}
