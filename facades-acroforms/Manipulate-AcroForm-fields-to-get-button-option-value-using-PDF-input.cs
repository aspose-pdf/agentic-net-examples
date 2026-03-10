using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the PDF containing the AcroForm
        using (Form form = new Form(inputPdf))
        {
            // Specify the fully qualified name of the button (radio group) field
            const string buttonFieldName = "RadioGroup1";

            // Retrieve the currently selected option value
            string currentValue = form.GetButtonOptionCurrentValue(buttonFieldName);
            Console.WriteLine($"Current value of '{buttonFieldName}': {currentValue}");

            // Retrieve all option values for the button field
            Dictionary<string, string> optionValues = form.GetButtonOptionValues(buttonFieldName);
            Console.WriteLine($"All option values for '{buttonFieldName}':");
            foreach (KeyValuePair<string, string> kvp in optionValues)
            {
                Console.WriteLine($"  Item name: {kvp.Key}, Export value: {kvp.Value}");
            }

            // If you need to change the selected option, uncomment the following line:
            // form.FillField(buttonFieldName, "NewOptionValue");

            // Save changes back to a new PDF if any modifications were made:
            // form.Save("output.pdf");
        }
    }
}