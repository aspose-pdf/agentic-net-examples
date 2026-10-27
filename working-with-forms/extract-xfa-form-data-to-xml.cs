using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using System.Xml;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "xfa_data.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the PDF contains an XFA form
                if (doc.Form != null && doc.Form.HasXfa)
                {
                    // Access the XFA packet via the core API (uppercase XFA)
                    XFA xfa = doc.Form.XFA;
                    XmlNode datasets = xfa.Datasets;

                    if (datasets != null)
                    {
                        // Save the XFA datasets (XML) to a file
                        File.WriteAllText(outputXml, datasets.OuterXml);
                        Console.WriteLine($"XFA data saved to '{outputXml}'.");
                    }
                    else
                    {
                        Console.WriteLine("XFA packet does not contain datasets.");
                    }
                }
                else
                {
                    Console.WriteLine("No XFA form found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
