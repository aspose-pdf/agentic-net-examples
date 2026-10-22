using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

// Stub implementation for PdfFormEditor when the Facades assembly is not referenced.
// This wrapper uses the Aspose.Pdf.Forms API to provide the same functionality
// required by the original sample (AddListBox and AddListItem).
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor
    {
        private Document _doc;

        /// <summary>
        /// Binds the editor to an existing Aspose.Pdf.Document instance.
        /// </summary>
        public void BindPdf(Document doc)
        {
            _doc = doc ?? throw new ArgumentNullException(nameof(doc));
        }

        /// <summary>
        /// Adds a list‑box field to the specified page.
        /// </summary>
        public void AddListBox(string fieldName, int pageNumber, Rectangle rect)
        {
            if (_doc == null) throw new InvalidOperationException("Document is not bound.");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentException("Field name cannot be null or empty.", nameof(fieldName));
            if (pageNumber < 1 || pageNumber > _doc.Pages.Count) throw new ArgumentOutOfRangeException(nameof(pageNumber));

            // Create the ListBoxField. No need to set Options – the Options property is read‑only.
            var listBox = new ListBoxField(_doc.Pages[pageNumber], rect)
            {
                PartialName = fieldName,
                MultiSelect = false
            };

            // Add the field to the form.
            _doc.Form.Add(listBox, pageNumber);
        }

        /// <summary>
        /// Adds a single item to an existing list‑box field.
        /// </summary>
        public void AddListItem(string fieldName, string item)
        {
            if (_doc == null) throw new InvalidOperationException("Document is not bound.");
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentException("Field name cannot be null or empty.", nameof(fieldName));
            if (item == null) throw new ArgumentNullException(nameof(item));

            // Locate the list‑box field via the Form.Fields collection (not via WidgetAnnotation).
            ListBoxField listBox = null;
            foreach (var field in _doc.Form.Fields)
            {
                if (field is ListBoxField lb && lb.PartialName == fieldName)
                {
                    listBox = lb;
                    break;
                }
            }

            if (listBox == null)
                throw new InvalidOperationException($"List box field '{fieldName}' not found.");

            // Use AddOption – Options is read‑only.
            listBox.AddOption(item);
        }
    }
}

class Program
{
    static void Main()
    {
        // Path for the output PDF
        const string outputPath = "CountryList.pdf";

        // List of 195 country names (ISO 3166‑1 short names)
        string[] countries = new string[]
        {
            "Afghanistan","Albania","Algeria","Andorra","Angola","Antigua and Barbuda","Argentina","Armenia","Australia","Austria",
            "Azerbaijan","Bahamas","Bahrain","Bangladesh","Barbados","Belarus","Belgium","Belize","Benin","Bhutan",
            "Bolivia (Plurinational State of)","Bosnia and Herzegovina","Botswana","Brazil","Brunei Darussalam","Bulgaria","Burkina Faso","Burundi","Cabo Verde","Cambodia",
            "Cameroon","Canada","Central African Republic","Chad","Chile","China","Colombia","Comoros","Congo","Congo (Democratic Republic of the)",
            "Costa Rica","Côte d’Ivoire","Croatia","Cuba","Cyprus","Czechia","Denmark","Djibouti","Dominica","Dominican Republic",
            "Ecuador","Egypt","El Salvador","Equatorial Guinea","Eritrea","Estonia","Eswatini","Ethiopia","Fiji","Finland",
            "France","Gabon","Gambia","Georgia","Germany","Ghana","Greece","Grenada","Guatemala","Guinea",
            "Guinea‑Bissau","Guyana","Haiti","Holy See","Honduras","Hungary","Iceland","India","Indonesia","Iran (Islamic Republic of)",
            "Iraq","Ireland","Israel","Italy","Jamaica","Japan","Jordan","Kazakhstan","Kenya","Kiribati",
            "Korea (Democratic People’s Republic of)","Korea (Republic of)","Kuwait","Kyrgyzstan","Lao People’s Democratic Republic","Latvia","Lebanon","Lesotho","Liberia","Libya",
            "Liechtenstein","Lithuania","Luxembourg","Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Marshall Islands",
            "Mauritania","Mauritius","Mexico","Micronesia (Federated States of)","Monaco","Mongolia","Montenegro","Morocco","Mozambique","Myanmar",
            "Namibia","Nauru","Nepal","Netherlands","New Zealand","Nicaragua","Niger","Nigeria","North Macedonia","Norway",
            "Oman","Pakistan","Palau","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Poland","Portugal",
            "Qatar","Republic of Korea","Republic of Moldova","Romania","Russian Federation","Rwanda","Saint Kitts and Nevis","Saint Lucia","Saint Vincent and the Grenadines","Samoa",
            "San Marino","Sao Tome and Principe","Saudi Arabia","Senegal","Serbia","Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia",
            "Solomon Islands","Somalia","South Africa","South Sudan","Spain","Sri Lanka","Sudan","Suriname","Sweden","Switzerland",
            "Syrian Arab Republic","Tajikistan","Thailand","Timor‑Leste","Togo","Tonga","Trinidad and Tobago","Tunisia","Turkey","Turkmenistan",
            "Tuvalu","Uganda","Ukraine","United Arab Emirates","United Kingdom of Great Britain and Northern Ireland","United Republic of Tanzania","United States of America","Uruguay","Uzbekistan","Vanuatu",
            "Venezuela (Bolivarian Republic of)","Viet Nam","Yemen","Zambia","Zimbabwe"
        };

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath)) ?? ".";
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Define the rectangle where the list box will appear (llx, lly, urx, ury)
            Rectangle listRect = new Rectangle(100, 500, 300, 700);

            // Use the stubbed PdfFormEditor to add a list box field named "CountryList"
            PdfFormEditor formEditor = new PdfFormEditor();
            formEditor.BindPdf(doc);
            formEditor.AddListBox("CountryList", 1, listRect);

            // Populate the list box with country names using AddListItem in a loop
            foreach (string country in countries)
            {
                formEditor.AddListItem("CountryList", country);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with populated CountryList saved to '{outputPath}'.");
    }
}
