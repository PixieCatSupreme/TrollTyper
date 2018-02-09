chatHandle = "doomedHeritic"
name = "Neswel"

chatColor = Color.Bronze

replacements = 
{
	TT.CreateReplacement("d", "D"),
	TT.CreateReplacement("o", "O"),
	TT.CreateReplacement("m", "M"),
	TT.CreateReplacement("b", "d"),
	TT.CreateReplacement("p", "q"),
	TT.CreateReplacement("DOOM", "[/color][b][color=#" .. TT.ColorToHex(Color.Black) .. "]DOOM[/color][/b][color=#" .. TT.ColorToHex(chatColor) .. "]", false, true)
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