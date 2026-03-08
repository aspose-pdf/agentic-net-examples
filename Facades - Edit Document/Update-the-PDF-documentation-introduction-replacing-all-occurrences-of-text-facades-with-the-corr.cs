using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade namespace for PDF editing

class UpdateDocumentationIntro
{
    static void Main()
    {
        // Path to the PDF file that contains the documentation introduction.
        const string inputPdfPath  = "DocumentationIntro.pdf";
        const string outputPdfPath = "DocumentationIntro_Updated.pdf";

        // Verify that the source file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPdfPath);

        // Create a PdfContentEditor facade to edit the PDF content.
        // The facade works on the loaded Document instance.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(doc);

        // Replace every occurrence of the phrase "text facades" with the correct term "Aspose.Pdf.Facades".
        // The page number 0 means "all pages".
        bool replaced = editor.ReplaceText(
            srcString: "text facades",
            thePage: 0,
            destString: "Aspose.Pdf.Facades");

        // Inform the user about the operation result.
        Console.WriteLine(replaced
            ? "All occurrences of \"text facades\" were replaced."
            : "No occurrences of \"text facades\" were found.");

        // Save the modified document.
        doc.Save(outputPdfPath);

        // Clean up resources.
        editor.Close();
        doc.Dispose();

        Console.WriteLine($"Updated documentation saved to '{outputPdfPath}'.");
    }
}