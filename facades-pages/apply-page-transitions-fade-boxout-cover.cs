using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade and bind it to the loaded document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // ----- Page 1: Fade effect (implemented as DISSOLVE) -----
            editor.ProcessPages = new int[] { 1 };               // edit only page 1
            editor.TransitionType = PdfPageEditor.DISSOLVE;      // Fade effect
            editor.TransitionDuration = 2;                       // duration in seconds
            editor.ApplyChanges();                               // apply changes to the page

            // ----- Page 2: BoxOut effect (implemented as OUTBOX) -----
            editor.ProcessPages = new int[] { 2 };
            editor.TransitionType = PdfPageEditor.OUTBOX;       // BoxOut effect
            editor.TransitionDuration = 2;
            editor.ApplyChanges();

            // ----- Page 3: Cover effect (implemented as INBOX) -----
            editor.ProcessPages = new int[] { 3 };
            editor.TransitionType = PdfPageEditor.INBOX;        // Cover effect
            editor.TransitionDuration = 2;
            editor.ApplyChanges();

            // Save the modified PDF to the output file
            doc.Save(outputPath);
            editor.Close(); // release resources held by the facade
        }

        Console.WriteLine($"Transitions applied and saved to '{outputPath}'.");
    }
}