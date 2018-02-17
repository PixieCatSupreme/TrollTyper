chatHandle = "aquireCardridges"
name = "Barbra"

chatColor = Color.Jade 

WordStutterScalar = 2

replacements = 
{
	TT.CreateReplacement("o", "0"),
	TT.CreateReplacement("l", "1")
}

function PreQuirk(text)
	math.randomseed(TT.GetSeed(text))
	
	words = TT.SplitWords(text);
	sentenceLenght = TT.GetArrayLenght(words) 
	
	for i = 1, sentenceLenght / WordStutterScalar +1, 1 do
	    wordIndex = math.random(1, sentenceLenght)
	    word = words[wordIndex]
	
	    stutter =  string.sub(word, 1, 1) .. VowelStutter(word)
	    words[wordIndex] = stutter .. "-" .. word 
	end
	
	text = "";
	
	for i = 1, sentenceLenght, 1 do
	    text = text.. " " .. words[i]
	end

	return text
end

function VowelStutter(word)
	if string.len(word) > 1 and TT.IsVowel(string.sub(word, 1, 1)) then
		return string.sub(word, 1, 1)
	end
	return ""
end