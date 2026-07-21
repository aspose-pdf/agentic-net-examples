using System;
using System.IO;
using Aspose.Pdf; // Document, Page
using Aspose.Pdf.Facades; // PdfPageEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade and bind the loaded document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Example: use page bookmarks as titles (if any). For simplicity, we'll use a placeholder title.
            // In a real scenario, retrieve the actual title for each page (e.g., from bookmarks or structure).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Placeholder: generate a title based on page number
                string pageTitle = $"Page {i} Title";

                // Determine transition duration based on title length (e.g., 1 second per 10 characters)
                int duration = Math.Max(1, pageTitle.Length / 10);

                // Set the page to be processed
                editor.ProcessPages = new int[] { i };

                // Apply transition settings
                editor.TransitionDuration = duration;          // duration in seconds
                editor.TransitionType = PdfPageEditor.BLINDV; // example transition style

                // Apply changes for this page
                editor.ApplyChanges();
            }

            // Save the modified PDF
            editor.Save(outputPath);
            editor.Close(); // optional, releases resources
        }

        Console.WriteLine($"PDF saved with transitions to '{outputPath}'.");
    }
}