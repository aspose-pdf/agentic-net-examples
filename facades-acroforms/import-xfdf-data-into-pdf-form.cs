using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF with form fields
        const string xfdfPath = "data.xfdf";       // XFDF file containing field values
        const string outputPdfPath = "output.pdf"; // PDF after importing data

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"Error: XFDF file not found – {xfdfPath}");
            return;
        }

        // Form is a Facade class that works with PDF documents.
        // It implements IDisposable, so wrap it in a using block for deterministic cleanup.
        using (Form form = new Form())
        {
            // Bind the existing PDF document to the Form object.
            form.BindPdf(inputPdfPath);

            // Import form field values from the XFDF file.
            // The ImportXfdf overload expects a Stream, so open the XFDF file as a read‑only stream.
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                form.ImportXfdf(xfdfStream);
            }

            // Save the updated PDF to a new file.
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdfPath}'.");
    }
}
