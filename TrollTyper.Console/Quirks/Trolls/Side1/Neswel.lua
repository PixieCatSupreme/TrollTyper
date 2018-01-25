chatHandle = "doomedHeritic"
name = "Neswel"

chatColor = Color.Bronze
black = Utilities.CreateColor(0, 0, 0)

replacements = 
{
	ValueReplacement("d", "D"),
	ValueReplacement("o", "O"),
	ValueReplacement("m", "M"),
	ValueReplacement("b", "d"),
	ValueReplacement("p", "q"),
	ValueReplacement("DOOM", "[/color][b][color=#" .. Utilities.ColorToHex(black) .. "]DOOM[/color][/b][color=#" .. Utilities.ColorToHex(chatColor) .. "]", false, true)
}

function PreQuirk(text)
	sentences = Utilities.SplitSentences(text);

	text = "";

	sentencesLenght = Utilities.GetArrayLenght(sentences) 
	
	for i = 0, sentencesLenght-1 , 1 do
	    sentence = sentences[i];
	    if not Utilities.IsNullOrWhiteSpace(sentence) then
	        words = Utilities.CountWords(sentence);
	
	        text = text .. sentence .. AddPunctuations(string.sub(sentence, -1), words - 1);
		end
	end
	return text
end

function AddPunctuations(punctuation, count)
	output = ""

	for i = 0, count-1, 1 do
		output = output .. punctuation
	end

	return output
end