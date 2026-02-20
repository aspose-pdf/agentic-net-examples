using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Name of the radio button field whose current option value we want to read
        const string fieldName = "RadioGroup1";

        // Verify that the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Initialize the Form facade and bind it to the PDF document
            using (Form form = new Form())
            {
                form.BindPdf(pdfPath);

                // Retrieve the current selected option value for the specified radio button field
                string currentValue = form.GetButtonOptionCurrentValue(fieldName);

                // If the field does not exist or has no selected option, GetButtonOptionCurrentValue returns null
                if (currentValue == null)
                {
                    Console.WriteLine($"Field '{fieldName}' not found or no option is selected.");
                }
                else
                {
                    Console.WriteLine($"Current value of button field '{fieldName}': {currentValue}");
                }

                // Optionally, retrieve all possible option values for the field
                Dictionary<string, string> allValuesDict = form.GetButtonOptionValues(fieldName);
                if (allValuesDict != null && allValuesDict.Count > 0)
                {
                    // Extract only the option values (the dictionary values) as a string array
                    string[] allValues = allValuesDict.Values.ToArray();
                    Console.WriteLine($"All possible values for '{fieldName}': {string.Join(", ", allValues)}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}
