using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // PDF containing numeric fields
        const string outputPath = "output_with_total.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Ensure automatic recalculation is enabled (default is true)
            form.AutoRecalculate = true;

            // Example field names – replace with actual field names in your PDF
            const string fieldName1 = "Amount1";
            const string fieldName2 = "Amount2";
            const string totalFieldName = "TotalAmount";

            // Retrieve the numeric fields (they are TextBoxField derivatives)
            TextBoxField field1 = form[fieldName1] as TextBoxField;
            TextBoxField field2 = form[fieldName2] as TextBoxField;
            TextBoxField totalField = form[totalFieldName] as TextBoxField;

            if (field1 == null || field2 == null || totalField == null)
            {
                Console.Error.WriteLine("One or more required fields were not found in the PDF form.");
                return;
            }

            // JavaScript that sums the two numeric fields and writes the result to the total field
            // The script runs in the context of the PDF viewer (Acrobat JavaScript)
            string jsCode = $@"
                var f1 = this.getField('{fieldName1}');
                var f2 = this.getField('{fieldName2}');
                var total = 0;
                // Parse values as numbers; treat empty or non‑numeric as 0
                if (f1 && !isNaN(parseFloat(f1.value))) total += parseFloat(f1.value);
                if (f2 && !isNaN(parseFloat(f2.value))) total += parseFloat(f2.value);
                var totalField = this.getField('{totalFieldName}');
                if (totalField) totalField.value = total.toString();
            ";

            // Create a JavascriptAction with the script (constructor requires the script string)
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Attach the JavaScript to the total field so it runs whenever the field is calculated
            totalField.ExecuteFieldJavaScript(jsAction);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with total calculation: '{outputPath}'");
    }
}
