using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string fieldName = "RadioGroup1"; // replace with the actual button field name

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF into a Form object
        using (Form form = new Form())
        {
            form.BindPdf(inputPdf);

            // Get all option values for the specified button field
            Dictionary<string, string> options = form.GetButtonOptionValues(fieldName);
            Console.WriteLine($"Options for field '{fieldName}':");
            foreach (KeyValuePair<string, string> kvp in options)
            {
                Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
            }

            // Get the currently selected option value
            string currentValue = form.GetButtonOptionCurrentValue(fieldName);
            Console.WriteLine($"Current selected value: {currentValue}");
        }
    }
}