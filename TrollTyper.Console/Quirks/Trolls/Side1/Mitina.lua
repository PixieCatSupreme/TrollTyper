chatHandle = "huntressAnimal"
name = "Mitina"

chatColor = Color.Burgundy

replacements = 
{
	  ValueReplacement("W", "oWo", true),
	  ValueReplacement("l", "w"),
	  ValueReplacement("L", "W"),
	  ValueReplacement("r", "w"),
	  ValueReplacement("R", "W"),
	  ValueReplacement("n", "ny"),
	  ValueReplacement("N", "Ny"),
	  ValueReplacement("you", "U", true)
}

function PostQuirk(text)
	words = Utilities.SplitWords(text);
	sentenceLenght = Utilities.GetArrayLenght(words) 
	
	text = ""
	
	for i = 0, sentenceLenght -1, 1 do
	    word = words[i]
		char = string.sub(word, 1, 1)

		if char ~= "w" and Utilities.IsVowel(string.sub(word, 2, 2)) then
			text = text .. " " .. char .. "w" .. string.sub(word, 2, string.len(word))
		else
			text = text .. " " .. word
		end
	end

	return text
end
