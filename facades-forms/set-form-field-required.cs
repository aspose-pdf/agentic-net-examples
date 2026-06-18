using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor resides here
using Aspose.Pdf;          // PropertyFlag enum

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the form
        const string outputPdf = "output.pdf";  // PDF after setting the required flag

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Set the "Agreement" field as required.
            // This also triggers PDF viewers to display the asterisk indicator.
            bool success = formEditor.SetFieldAttribute("Agreement", PropertyFlag.Required);

            if (!success)
            {
                Console.Error.WriteLine("Failed to set the required attribute on the field 'Agreement'.");
            }

            // Persist changes to the output file.
            formEditor.Save();
        }

        Console.WriteLine($"Field 'Agreement' marked as required. Output saved to '{outputPdf}'.");
    }
}