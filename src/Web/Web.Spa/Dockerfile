FROM node:20.12.2-alpine

WORKDIR /usr/src/app
EXPOSE 80
EXPOSE 443

COPY . /usr/src/app

RUN npm install -g @angular/cli

RUN npm install

CMD ["ng", "serve", "--host", "0.0.0.0"]
