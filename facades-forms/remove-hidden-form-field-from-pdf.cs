using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF containing the hidden field
        const string outputPath = "output.pdf";     // PDF after the field is removed
        const string hiddenFieldName = "HiddenField"; // name of the hidden form field to delete

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // FormEditor works directly on PDF files; no Document object is needed.
            FormEditor formEditor = new FormEditor();

            // Load the PDF into the editor
            formEditor.BindPdf(inputPath);

            // Remove the specified hidden field (if it exists)
            formEditor.RemoveField(hiddenFieldName);

            // Save the modified PDF
            formEditor.Save(outputPath);

            Console.WriteLine($"Hidden field \"{hiddenFieldName}\" removed. Saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}