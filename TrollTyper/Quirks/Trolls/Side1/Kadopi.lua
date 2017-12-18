chatHandle = "bitsViewer"
name = "Kadopi"

chatColor = Utilities.CreateColor(161,161,0)

replacements = 
{
	ValueReplacement("z", "2"),
	ValueReplacement("i", "1"),
	ValueReplacement("a", "4"),
	ValueReplacement("t", "7"),
	ValueReplacement("h", "4"),
	ValueReplacement("b", "8"),
	ValueReplacement("e", "3"),
	ValueReplacement("g", "6"),
	ValueReplacement("q", "4"),
	ValueReplacement("7", "l")
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
