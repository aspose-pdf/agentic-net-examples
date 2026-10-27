using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string listBoxName = "myListBox"; // name of the list box field in the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the list box field from the form collection using the Fields collection
            var listBox = doc.Form?.Fields?
                .FirstOrDefault(f => string.Equals(f.Name, listBoxName, StringComparison.Ordinal))
                as ListBoxField;

            if (listBox != null)
            {
                // Ensure the list box allows multiple selections
                listBox.MultiSelect = true;

                // Set the maximum number of selectable items to three.
                // The property is called MaxSelection in newer versions; for older versions we set it via reflection.
                const int maxSelection = 3;
                PropertyInfo maxSelProp = listBox.GetType().GetProperty("MaxSelection", BindingFlags.Public | BindingFlags.Instance);
                if (maxSelProp != null && maxSelProp.CanWrite)
                {
                    maxSelProp.SetValue(listBox, maxSelection);
                }
                else
                {
                    // Fallback: add a simple JavaScript validation that limits the selection count.
                    string js = $"if (event.target.value.split(',').length > {maxSelection}) {{ app.alert('Maximum {maxSelection} items can be selected.'); event.target.value = event.target.value.split(',').slice(0,{maxSelection}).join(','); }}";
                    // OnValidate expects a PdfAction; use JavascriptAction to wrap the script.
                    listBox.Actions.OnValidate = new JavascriptAction(js);
                }
            }
            else
            {
                Console.Error.WriteLine($"List box field '{listBoxName}' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
