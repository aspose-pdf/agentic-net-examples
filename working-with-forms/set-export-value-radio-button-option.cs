using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the radio button
        const string outputPdf = "output.pdf";         // PDF after setting the export value
        const string radioFieldName = "MyRadioGroup"; // name of the RadioButtonField
        const string optionName = "OptionA";          // visible name of the option to modify
        const string exportCode = "CODE123";          // downstream system code to set as export value

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPdf))
        {
            // Access the form and retrieve the radio button field
            Form form = doc.Form;
            RadioButtonField radio = form[radioFieldName] as RadioButtonField;

            if (radio == null)
            {
                Console.Error.WriteLine($"Radio button field '{radioFieldName}' not found.");
            }
            else
            {
                // Iterate over the options of the radio button and set the export value
                foreach (Aspose.Pdf.Forms.Option opt in radio.Options)
                {
                    if (opt.Name == optionName)
                    {
                        // Set the export value that downstream systems will receive
                        opt.Value = exportCode;
                        Console.WriteLine($"Set export value of option '{optionName}' to '{exportCode}'.");
                        break;
                    }
                }
            }

            // Save the modified PDF (PDF format, no special SaveOptions needed)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}