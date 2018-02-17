chatHandle = "codingHobbyist"
name = "Kadopi"

chatColor = Color.Yellow

replacements = 
{
	TT.CreateReplacement("s", "2"),
	TT.CreateReplacement("i", "1"),
	TT.CreateReplacement("a", "4"),
	TT.CreateReplacement("t", "7"),
	TT.CreateReplacement("b", "8"),
	TT.CreateReplacement("e", "3"),
	TT.CreateReplacement("g", "6"),
}

function PreQuirk(text)
	words = TT.SplitWords(text);
	sentenceLenght = TT.GetArrayLenght(words) 
	
	text = ""
	
	for i = 1, sentenceLenght, 1 do
	    word = words[i]
		text = text .. " " .. string.upper(string.sub(word, 1, 1)) .. string.sub(word, 2, string.len(word))
	end

	return text
end
