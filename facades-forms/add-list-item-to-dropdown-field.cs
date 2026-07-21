using System;
using System.IO;
using Aspose.Pdf.Facades; // FormEditor resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the dropdown field "Choices" on page 4
        const string outputPdf = "output.pdf";  // PDF after the new item is added

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        // The constructor takes the source PDF and the destination PDF.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a new item "Option A" to the dropdown (combo box) field named "Choices".
            // The field name must be the fully qualified name; here we assume "Choices" is sufficient.
            formEditor.AddListItem("Choices", "Option A");

            // Persist changes to the output file.
            formEditor.Save();
        }

        Console.WriteLine($"Added \"Option A\" to dropdown \"Choices\" and saved as '{outputPdf}'.");
    }
}