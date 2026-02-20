using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Path to the PDF file that contains AcroForm fields
        string pdfPath = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath}");
            return;
        }

        try
        {
            // Create a Form facade instance and bind it to the PDF document
            using (Form form = new Form())
            {
                // Load the PDF into the facade
                form.BindPdf(pdfPath);

                // Retrieve the collection of field names (IEnumerable<string>)
                var fieldNames = form.FieldNames;

                // Convert to a concrete list to simplify null‑checking and counting
                var names = fieldNames?.ToList() ?? new List<string>();

                // Output the field names (if any)
                if (names.Count == 0)
                {
                    Console.WriteLine("No AcroForm fields were found in the document.");
                }
                else
                {
                    Console.WriteLine("AcroForm field names:");
                    foreach (string name in names)
                    {
                        Console.WriteLine(name);
                    }
                }

                // No modifications are made, but the document can be saved if desired:
                // form.Save("output.pdf"); // uses the SaveableFacade Save method
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}