using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Aspose.Pdf API (no Facades).
        Document pdfDocument = new Document(inputPath);

        // ----- Modify a text field -----
        if (pdfDocument.Form["CustomerName"] is TextBoxField txtField)
        {
            txtField.Value = "Acme Corp";
        }
        else
        {
            Console.Error.WriteLine("Text field 'CustomerName' not found or is not a TextBoxField.");
        }

        // ----- Modify a checkbox field -----
        if (pdfDocument.Form["AcceptTerms"] is CheckboxField chkField)
        {
            // The Checked property sets the state of the checkbox.
            chkField.Checked = true;
        }
        else
        {
            Console.Error.WriteLine("Check box field 'AcceptTerms' not found or is not a CheckboxField.");
        }

        // ----- Modify a radio‑button field -----
        // Radio buttons are represented by RadioButtonOptionField objects.
        // To select an option, set the OptionName to the name of the desired option.
        if (pdfDocument.Form["Gender"] is RadioButtonOptionField radioOption)
        {
            // Select the option named "Male".
            radioOption.OptionName = "Male";
            // No separate "Selected" property exists; setting OptionName is sufficient.
        }
        else
        {
            Console.Error.WriteLine("Radio button field 'Gender' not found or is not a RadioButtonOptionField.");
        }

        // Save the updated PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Form fields updated and saved to '{outputPath}'.");
    }
}
