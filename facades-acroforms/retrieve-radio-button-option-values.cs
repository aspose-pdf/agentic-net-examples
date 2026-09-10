using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "PdfForm.pdf";

        // Verify the PDF file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF form using the Form facade (IDisposable, so wrap in using)
        using (Form form = new Form(pdfPath))
        {
            // Retrieve the radio button option values for the specified field name
            // The method returns a Dictionary<string, string>
            Dictionary<string, string> dict = form.GetButtonOptionValues("Color");

            // Convert the Dictionary to a Hashtable as required
            Hashtable values = new Hashtable();
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                values.Add(kvp.Key, kvp.Value);
            }

            // Example: display the retrieved option values
            foreach (object key in values.Keys)
            {
                Console.WriteLine($"{key}: {values[key]}");
            }
        }
    }
}