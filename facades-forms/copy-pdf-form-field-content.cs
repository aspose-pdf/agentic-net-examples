using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF form
        using (Form form = new Form(inputPdf))
        {
            // Retrieve the value from the source field
            string sourceValue = form.GetField("SourceNotes");

            // If the source field exists, copy its value to the target field
            if (sourceValue != null)
            {
                bool filled = form.FillField("TargetNotes", sourceValue);
                if (!filled)
                {
                    Console.Error.WriteLine("Target field not found or could not be filled.");
                }
            }
            else
            {
                Console.Error.WriteLine("Source field not found.");
            }

            // Save the updated PDF
            form.Save(outputPdf);
        }

        Console.WriteLine($"Field content copied and saved to '{outputPdf}'.");
    }
}