using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldName    = "OldName";
        const string newName    = "NewName";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor is the correct class for editing form fields (it implements IDisposable)
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPath);

                // Rename every occurrence of the field "OldName" to "NewName"
                formEditor.RenameField(oldName, newName);

                // Save the updated PDF
                formEditor.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
