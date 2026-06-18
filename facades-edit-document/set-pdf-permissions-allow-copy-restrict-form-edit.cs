using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileSecurity facade
        PdfFileSecurity security = new PdfFileSecurity();

        // Bind the source PDF file
        security.BindPdf(inputPdf);

        // Create a privilege object based on the predefined "AllowAll" preset
        DocumentPrivilege privilege = DocumentPrivilege.AllowAll;

        // Allow copying/extracting text and graphics
        privilege.AllowCopy = true;

        // Disallow form field editing (filling) and annotation modifications
        privilege.AllowFillIn = false;
        privilege.AllowModifyAnnotations = false;

        // Apply the privilege settings to the PDF
        bool success = security.SetPrivilege(privilege);
        if (!success)
        {
            Console.Error.WriteLine("Failed to set PDF privileges.");
            security.Close();
            return;
        }

        // Save the secured PDF to the output path
        security.Save(outputPdf);

        // Release resources held by the facade
        security.Close();

        Console.WriteLine($"PDF permissions updated and saved to '{outputPdf}'.");
    }
}