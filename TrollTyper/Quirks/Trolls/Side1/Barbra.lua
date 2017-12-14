chatHandle = "cardridgeCollector"

chatColor = Utilities.CreateColor(161, 0, 0)

WordStutterScalar = 2

function PreQuirk(text)
	math.randomseed(Utilities.GetSeed(text))
	
	words = Utilities.SplitWords(text);
	sentenceLenght = Utilities.GetArrayLenght(words) 
	
	for i = 0, sentenceLenght / WordStutterScalar, 1 do
	    wordIndex = math.random(0, sentenceLenght-1)
	    word = words[wordIndex]
	
	    stutter =  string.sub(word, 1, 1) .. VowelStutter(word)
	    words[wordIndex] = stutter .. "-" .. word 
	end
	
	text = "";
	
	for i = 0, sentenceLenght-1, 1 do
	    text = text.. " " .. words[i]
	end

	return text
end

function VowelStutter(word)
	if string.len(word) > 1 and Utilities.IsVowel(string.sub(word, 1, 1)) then
		return string.sub(word, 1, 1)
	end
	return ""
end