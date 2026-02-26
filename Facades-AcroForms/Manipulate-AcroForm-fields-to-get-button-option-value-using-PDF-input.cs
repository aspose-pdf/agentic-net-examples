using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string buttonFieldName = "Color"; // replace with your radio button group name

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Wrap Form in a using block for deterministic disposal
        using (Form form = new Form(inputPdf))
        {
            // Retrieve the currently selected option value of the radio button group
            string currentValue = form.GetButtonOptionCurrentValue(buttonFieldName);
            Console.WriteLine($"Current value of '{buttonFieldName}': {currentValue}");

            // Retrieve all possible option values (item name -> export value)
            var optionValues = form.GetButtonOptionValues(buttonFieldName);
            Console.WriteLine($"All option values for '{buttonFieldName}':");
            foreach (var kvp in optionValues)
            {
                Console.WriteLine($"  Item: {kvp.Key}, Export Value: {kvp.Value}");
            }
        }
    }
}