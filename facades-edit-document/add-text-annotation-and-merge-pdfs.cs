using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string editedFirstTempPath = "first_edited.pdf";
        const string outputPdfPath = "merged_output.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load first PDF, add an annotation, and save to a temporary file
        using (Document firstDoc = new Document(firstPdfPath))
        {
            // Create a simple text annotation on the first page
            Page page = firstDoc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "Added annotation",
                Color = Aspose.Pdf.Color.Yellow,
                Open = true,
                Icon = TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // Save the edited first document to a temporary file
            firstDoc.Save(editedFirstTempPath);
        }

        // Concatenate the edited first PDF with the second PDF using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Concatenate(editedFirstTempPath, secondPdfPath, outputPdfPath);

        if (success)
        {
            Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to merge PDFs.");
        }

        // Clean up temporary file
        try
        {
            if (File.Exists(editedFirstTempPath))
                File.Delete(editedFirstTempPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}