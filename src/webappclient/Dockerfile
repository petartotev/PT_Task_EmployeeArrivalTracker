# pull official base image
FROM node:16.16.0-alpine

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY package.json ./
COPY package-lock.json ./
RUN npm install --silent
RUN npm install react-scripts@3.4.1 -g --silent

# add app
COPY . ./

# start app
CMD ["npm", "start"]

########################################

# docker build -t <docker-hub-id>/webappclientimage .
# docker push <docker-hub-id>/webappclientimage
# docker run -p 3000:3000 --name webappclientcontainer -d <docker-hub-id>/webappclientimage
# http://localhost:3000