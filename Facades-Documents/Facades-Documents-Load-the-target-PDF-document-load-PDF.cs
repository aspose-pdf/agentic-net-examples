using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            using (Form pdfForm = new Form())
            {
                pdfForm.BindPdf(pdfPath);
                Console.WriteLine("PDF loaded successfully using Aspose.Pdf.Facades.");

                int fieldCount = 0;
                if (pdfForm.FieldNames != null)
                {
                    // FieldNames is an array; use Length to obtain the number of items.
                    fieldCount = pdfForm.FieldNames.Length;
                }

                Console.WriteLine($"Number of form fields: {fieldCount}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}
