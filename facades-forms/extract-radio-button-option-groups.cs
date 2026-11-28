using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormG.pdf";
        const string outputPath = "OptionGroupList.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Retrieve all radio‑button option fields (these represent the options of a group)
            var radioOptions = pdfDocument.Form?.Fields?.OfType<RadioButtonOptionField>();

            if (radioOptions == null || !radioOptions.Any())
            {
                Console.WriteLine("No option groups (radio button fields) were found.");
                return;
            }

            // Group the options by the field name – the name identifies the option group
            var groups = radioOptions.GroupBy(o => o.Name);

            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                foreach (var group in groups)
                {
                    writer.WriteLine($"OptionGroup: {group.Key}");
                    foreach (var option in group)
                    {
                        // OptionName is the label of the individual radio button; Value is the export value
                        writer.WriteLine($"  Option Name: {option.OptionName}, Value: {option.Value}");
                    }
                    writer.WriteLine(); // blank line between groups
                }
            }
        }

        Console.WriteLine($"Option groups have been written to '{outputPath}'.");
    }
}
