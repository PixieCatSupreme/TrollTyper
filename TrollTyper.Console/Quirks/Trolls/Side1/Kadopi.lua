chatHandle = "codingHobbyist"
name = "Kadopi"

chatColor = Color.Yellow

replacements = 
{
	ValueReplacement("s", "2"),
	ValueReplacement("i", "1"),
	ValueReplacement("a", "4"),
	ValueReplacement("t", "7"),
	ValueReplacement("b", "8"),
	ValueReplacement("e", "3"),
	ValueReplacement("g", "6"),
}

function PreQuirk(text)
	words = Utilities.SplitWords(text);
	sentenceLenght = Utilities.GetArrayLenght(words) 
	
	text = ""
	
	for i = 0, sentenceLenght -1, 1 do
	    word = words[i]
		text = text .. " " .. string.upper(string.sub(word, 1, 1)) .. string.sub(word, 2, string.len(word))
	end

	return text
end
