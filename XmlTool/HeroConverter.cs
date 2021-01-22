using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;
using static XmlTool.Program;

namespace XmlTool {
    public class HeroConverter {
		public static void heroes_CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			HeroRecord record = new HeroRecord();
			IEnumerable<HeroRecord> records = csv.EnumerateRecords(record);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Heroes");

				writeHeadderComment(writer);

				foreach (HeroRecord heroRecord in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Hero");

					//Changes
					record.is_noble = record.is_noble.ToLower();
					record.alive = record.alive.ToLower();

					//Temporary

					//Write
					writer.WriteAttributeString(null, "id", null, heroRecord.id);
					if (!heroRecord.is_noble.Equals("")) writer.WriteAttributeString(null, "is_noble", null, heroRecord.is_noble);
					if (!heroRecord.father.Equals("")) writer.WriteAttributeString(null, "father", null, heroRecord.father);
					if (!heroRecord.mother.Equals("")) writer.WriteAttributeString(null, "mother", null, heroRecord.mother);
					writer.WriteAttributeString(null, "faction", null, "Faction." + heroRecord.faction);
					if (!heroRecord.banner_key.Equals("")) writer.WriteAttributeString(null, "banner_key", null, heroRecord.banner_key);
					if (!heroRecord.spouse.Equals("")) writer.WriteAttributeString(null, "spouse", null, heroRecord.spouse);
					if (!heroRecord.alive.Equals("")) writer.WriteAttributeString(null, "alive", null, heroRecord.alive);
					if (!heroRecord.text.Equals("")) writer.WriteAttributeString(null, "text", null, heroRecord.text);

					if (!heroRecord.text.Equals("")) writeLocalizationNode(localizationWriter, "Heros.Hero." + record.id + ".text", record.text);

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class HeroRecord {
			public string id { get; set; }
			public string is_noble { get; set; }
			public string father { get; set; }
			public string mother { get; set; }
			public string faction { get; set; }
			public string banner_key { get; set; }
			public string spouse { get; set; }
			public string alive { get; set; }
			public string text { get; set; }
		}
	}
}
