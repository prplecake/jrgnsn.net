module ModificationDate
	class Generator < Jekyll::Generator
		def generate(site)

			# loop through pages
			site.pages.each do |page|

				# get current page last modified time
				# unless it's 'feed.xml'...
				unless ['feed.xml'].include? page.path
					# ...or starts with 'page'
					unless page.path.start_with? 'page'
						modification_time = File.mtime( page.path )
					end
				end

				# inject modification time in page data
				page.data['mdate'] = modification_time.to_s

			end
		end
	end
end