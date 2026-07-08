using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "OldReport.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF using the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Access the embedded files collection via the underlying Document
        EmbeddedFileCollection attachments = editor.Document.EmbeddedFiles;

        // Determine whether the specified attachment exists using reflection/dynamic to avoid direct type dependency
        bool exists = false;
        foreach (var fileObj in attachments)
        {
            // Use reflection to get the Name property (the concrete type may be internal)
            var nameProp = fileObj.GetType().GetProperty("Name");
            if (nameProp != null)
            {
                string name = nameProp.GetValue(fileObj) as string;
                if (string.Equals(name, attachmentName, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }
        }

        if (exists)
        {
            // Delete the specific attachment by name
            attachments.Delete(attachmentName);
            Console.WriteLine($"Attachment '{attachmentName}' removed.");
        }
        else
        {
            Console.WriteLine($"Attachment '{attachmentName}' not found.");
        }

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
